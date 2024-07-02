using System.Runtime.InteropServices;

namespace RFTools.WinUi.Helpers;

internal class NativeHelper
{
    public const int ERROR_SUCCESS = 0;
    public const int ERROR_INSUFFICIENT_BUFFER = 122;
    public const int APPMODEL_ERROR_NO_PACKAGE = 15700;

    [DllImport("api-ms-win-appmodel-runtime-l1-1-1", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.U4)]
    internal static extern uint GetCurrentPackageId(ref int pBufferLength, out byte pBuffer);

    public static bool IsAppPackaged
    {
        get
        {
            int bufferSize = 0;
            byte byteBuffer = 0;
            uint lastError = NativeHelper.GetCurrentPackageId(ref bufferSize, out byteBuffer);
            bool isPackaged = lastError != APPMODEL_ERROR_NO_PACKAGE;

            return isPackaged;
        }
    }
}