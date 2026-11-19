using Bai6_5_KaraokeManagement.GUI;

namespace Bai6_5_KaraokeManagement;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new frmKaraokeMain());
    }
}
