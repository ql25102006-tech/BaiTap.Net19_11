using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace Bai6_3_ProjectTaskManagement.DAL
{
    public static class DbConnectionHelper
    {
        private static string? _resolvedConnStr;
        private static bool _dbInitialized;

        private static readonly string[] CandidateServers = new[]
        {
            @"np:\\.\pipe\MSSQL$QUYEN\sql\query",
            @"localhost\QUYEN",
            @".\QUYEN",
            @"np:\\.\pipe\MSSQL$SQLEXPRESS\sql\query",
            @"localhost\SQLEXPRESS",
            @".\SQLEXPRESS",
            @"localhost"
        };

        public static SqlConnection GetConnection()
        {
            EnsureDatabaseReady();
            return new SqlConnection(_resolvedConnStr);
        }

        private static void EnsureDatabaseReady()
        {
            if (_dbInitialized && !string.IsNullOrEmpty(_resolvedConnStr)) return;

            string? workingServer = null;
            foreach (var srv in CandidateServers)
            {
                string testMaster = $"Data Source={srv};Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                try
                {
                    using var conn = new SqlConnection(testMaster);
                    conn.Open();
                    workingServer = srv;
                    break;
                }
                catch { }
            }

            if (workingServer == null)
            {
                var setting = ConfigurationManager.ConnectionStrings["ProjectTaskDB"];
                _resolvedConnStr = setting?.ConnectionString ?? @"Data Source=localhost\QUYEN;Initial Catalog=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;";
                return;
            }

            // Tự động khởi tạo ProjectTaskDB nếu chưa có
            string masterConn = $"Data Source={workingServer};Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";
            using (var conn = new SqlConnection(masterConn))
            {
                conn.Open();
                using var cmdCheck = new SqlCommand("SELECT DB_ID('ProjectTaskDB')", conn);
                object? dbId = cmdCheck.ExecuteScalar();
                if (dbId == null || dbId == DBNull.Value)
                {
                    RunScriptFile(conn);
                }
            }

            _resolvedConnStr = $"Data Source={workingServer};Initial Catalog=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;";
            _dbInitialized = true;
        }

        private static void RunScriptFile(SqlConnection conn)
        {
            string? scriptPath = FindScriptPath();
            if (scriptPath == null || !File.Exists(scriptPath)) return;

            string script = File.ReadAllText(scriptPath);
            var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            foreach (var batch in batches)
            {
                if (string.IsNullOrWhiteSpace(batch)) continue;
                using var cmd = new SqlCommand(batch, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private static string? FindScriptPath()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6; i++)
            {
                string candidate = Path.Combine(dir, "Database", "ScriptDB.sql");
                if (File.Exists(candidate)) return candidate;
                var parent = Directory.GetParent(dir);
                if (parent == null) break;
                dir = parent.FullName;
            }
            return null;
        }
    }
}
