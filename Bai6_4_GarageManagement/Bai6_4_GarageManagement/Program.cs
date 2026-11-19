using Bai6_4_GarageManagement.GUI;

namespace Bai6_4_GarageManagement;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new frmGarageMain());
    }
}
