using Device.Net;
using Hid.Net.Windows;
using Backlighter.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MyCustomApplicationContext());
    }
  }

  public class MyCustomApplicationContext : ApplicationContext
  {
    private NotifyIcon trayIcon;
    private IDevice unifyingReceiver;

    public MyCustomApplicationContext ()
    {
      ToolStripMenuItem mnuExit = new ToolStripMenuItem("Exit");
      ContextMenuStrip MainMenu = new ContextMenuStrip();
      MainMenu.Items.AddRange(new ToolStripItem [] { mnuExit });
      mnuExit.Click += new System.EventHandler (clickExit);
		  
      trayIcon = new NotifyIcon();
      trayIcon.Icon = Resources.keyboard;
      trayIcon.ContextMenuStrip = MainMenu;
      trayIcon.Visible = true;
      
      start();
    }

    void clickExit(object sender, EventArgs e)
    {
      if (unifyingReceiver != null) unifyingReceiver.Dispose();
      trayIcon.Visible = false;
      Application.Exit();
    }

    public async void start()
    {
      //Register the factory for creating Hid devices. 
      var hidFactory =
        new FilterDeviceDefinition(vendorId: 0x046d, productId: 0xc52b, label: "Logitech Unifying Receiver", usagePage: 65280)
        .CreateWindowsHidDeviceFactory();

      //Get connected device definitions
      var deviceDefinitions = (await hidFactory.GetConnectedDeviceDefinitionsAsync().ConfigureAwait(false)).ToList();
      if (deviceDefinitions.Count == 0) return;

      //Get the device from its definition
      unifyingReceiver = await hidFactory.GetDeviceAsync(deviceDefinitions.First()).ConfigureAwait(false);

      //Initialize the device
      await unifyingReceiver.InitializeAsync().ConfigureAwait(false);

      //Signal data to turn on or off backlighting on MX Keys
      var off_signal = new byte[] { 0x10, 0x01, 0x0b, 0x1f, 0x00, 0x00, 0xff };
      var on_signal = new byte[] { 0x10, 0x01, 0x0b, 0x1f, 0x01, 0x00, 0xff };

      //Send the signals to turn off and on the backlighting
      while(true)
      {
        await unifyingReceiver.WriteAsync(off_signal).ConfigureAwait(false);
        Thread.Sleep(50);
        await unifyingReceiver.WriteAsync(on_signal).ConfigureAwait(false);
        Thread.Sleep(180000);
      }
    }
  }
}
