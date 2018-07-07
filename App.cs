#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

//Requires PresentationCore Addin to become active
using System.Windows.Media.Imaging;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion


namespace CustomButton
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            String tabName = "VantageTCG";
            application.CreateRibbonTab(tabName);

            // Create a ribbon panel; First arg adds panel to custom tab
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Tools");

            // Button details and its link to the command
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdName",
                "New Button Name", thisAssemblyPath, typeof(ExecuteCommand).FullName);

            // Does the same as the above
            // PushButtonData buttonData = new PushButtonData("cmdDeleteViews",
            //     "Delete Views", thisAssemblyPath, "RevitWarnings.DeleteViews");

            // Creates a Button
            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            pushButton.ToolTip = "How to use the button";

            // Adds picture for button
            string filename = @"\Views.png";
            string currentDir = AssemblyDirectory;
            //Uri uriImage = new Uri(@"%APPDATA%\Autodesk\Revit\Addins\2015\Views.png");
            //Uri uriImage = new Uri(@"C:\Users\rorymulcahey\AppData\Roaming\Autodesk\Revit\Addins\2015\Views.png");
            Uri uriImage = new Uri(currentDir + filename);
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;

            return Result.Succeeded; // must return
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded; // must return
        }

        // Finds the current directory
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }

    // Critical line for functionality
    [Transaction(TransactionMode.Manual)]

    // Command that will be executed upon button click event
    public class ExecuteCommand : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("Title", "Message");
            return Result.Succeeded; // must return here
        }
    }
}
