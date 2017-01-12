using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.async;
using WindowsFormsApplication1.stud;
using WindowsFormsApplication1.stud.extension_methods;
using WindowsFormsApplication1.stud.implicitly_typed;
using WindowsFormsApplication1.stud.partial_methods;
using WindowsFormsApplication1.stud.references_VB_my_class;
using WindowsFormsApplication1.stud.WFC;
using WindowsFormsApplication1.stud.enum_struct_nullable;


namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestProject());
        }
    }
}
