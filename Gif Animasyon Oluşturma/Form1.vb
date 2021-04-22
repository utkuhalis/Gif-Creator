Imports System.IO
Imports System.Drawing.Imaging
Public Class Form1

    Dim GifAnimation() As Byte = {33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0}

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim GifFolder As String = Application.StartupPath & "\Resimler\"
        Dim GifFile As String = Application.StartupPath & "\Resimler\ScreenShot.Gif"
        Dim Files() As String = Directory.GetFiles(GifFolder, "*.Jpg")
        Dim MS As New MemoryStream
        Dim BR As New BinaryReader(MS)
        Dim BW As New BinaryWriter(New FileStream(GifFile, FileMode.Create))
        Image.FromFile(Files(0)).Save(MS, ImageFormat.Gif)
        Dim B() As Byte = MS.ToArray
        B(10) = B(10) And &H78
        BW.Write(B, 0, 13)
        BW.Write(GifAnimation)
        WriteGifImg(B, BW)
        For I As Integer = 1 To Files.Length - 1
            MS.SetLength(0)
            Image.FromFile(Files(I)).Save(MS, ImageFormat.Gif)
            B = MS.ToArray
            WriteGifImg(B, BW)
        Next
        BW.Write(B(B.Length - 1))
        BW.Close()
        MS.Dispose()
        PictureBox1.Image = Image.FromFile(Application.StartupPath & "\Resimler\ScreenShot.Gif")

    End Sub

    Sub WriteGifImg(ByVal B() As Byte, ByVal BW As BinaryWriter)
        B(785) = 50 : B(786) = 0
        B(798) = B(798) Or &H87
        BW.Write(B, 781, 18)
        BW.Write(B, 13, 768)
        BW.Write(B, 799, B.Length - 800)
    End Sub
End Class
