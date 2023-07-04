using Camera.MAUI;

namespace Lynx.Views;

public partial class QRCodePage : ContentPage
{
	public QRCodePage()
	{
		InitializeComponent();
        camV.CamerasLoaded += CamV_CamerasLoaded;
        camV.BarcodeDetected += CamV_BarcodeDetected;
    }

    private void CamV_CamerasLoaded(object sender, EventArgs e)
    {
        if (    camV.NumCamerasDetected > 0)
        {
            camV.Camera = camV.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await camV.StopCameraAsync();
                await camV.StartCameraAsync();
            });
        }

    }

    private void CamV_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        Debug.WriteLine("QR CONTENT:"+args.Result[0].Text);

    }
}