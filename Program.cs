using System;
using System.Drawing;
using System.Text;

class Embutir
{
    static void Main()
    {
        Bitmap imagemOriginal = new Bitmap("imagem_original.png");
        string codigoParaEmbutir = "Hello, World!"; // Substitua pelo código desejado
        int bitPosicao = 0;
        int charIndex = 0;

        for (int i = 0; i < imagemOriginal.Width; i++)
        {
            if (charIndex >= codigoParaEmbutir.Length)
                break;

            Color pixel = imagemOriginal.GetPixel(i, 0);
            int r = pixel.R, g = pixel.G, b = pixel.B;

            for (int canal = 0; canal < 3; canal++)
            {
                if (charIndex < codigoParaEmbutir.Length)
                {
                    byte bit = (byte)((codigoParaEmbutir[charIndex] >> (7 - bitPosicao)) & 0x01);

                    if (canal == 0)
                        r = (r & 0xFE) | bit;
                    else if (canal == 1)
                        g = (g & 0xFE) | bit;
                    else
                        b = (b & 0xFE) | bit;

                    bitPosicao++;
                    if (bitPosicao == 8)
                    {
                        bitPosicao = 0;
                        charIndex++;
                    }
                }
            }

            Color novoPixel = Color.FromArgb(r, g, b);
            imagemOriginal.SetPixel(i, 0, novoPixel);
        }

        imagemOriginal.Save("imagem_com_codigo.png");
        Console.WriteLine("Dados embutidos com sucesso!");
    }
}
