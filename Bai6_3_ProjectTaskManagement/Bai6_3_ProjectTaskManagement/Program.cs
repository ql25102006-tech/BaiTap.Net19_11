using Bai6_3_ProjectTaskManagement.GUI;

namespace Bai6_3_ProjectTaskManagement;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new frmProjectTaskMain());
    }
}
