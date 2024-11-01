using PuppeteerSharp;
using PuppeteerSharp.Media;

public class GeneratePdfFromHtml
{
    private string headerTemplate = "";
    private string footerTemplate = "";
    private string html = "";
    private string fileFind = "chrome";
    private MarginOptions marginOption = new MarginOptions
    {
        Top = "1cm",
        Bottom = "1cm",
        Left = "1cm",
        Right = "1cm"
    };

    public GeneratePdfFromHtml(
            string html,
            string headerTemplate = "<div style='font-size:10px; width:100%; text-align:center;'></div>",
            string footerTemplate = "<div style='font-size:10px; width:100%; text-align:center;'>Página <span class='pageNumber'></span> de <span class='totalPages'></span></div>")
    {
        this.headerTemplate = headerTemplate;
        this.footerTemplate = footerTemplate;
        this.html = html;

        if (OperatingSystem.IsWindows())
        {
            fileFind = "chrome.exe";
        }
        else
        {
            fileFind = "chrome";
        }
    }
    public void SetHeader(string header = "<div style='font-size:10px; width:100%; text-align:center;'></div>")
    {
        headerTemplate = header;
    }
    public void SetFooter(string footer = "<div style='font-size:10px; width:100%; text-align:center;'>Página <span class='pageNumber'></span> de <span class='totalPages'></span></div>")
    {
        footerTemplate = footer;
    }

    public void SetMarginOptions(MarginOptions marginOpt)
    {
        marginOption = marginOpt;
    }
    public async Task<byte[]> GeneratePdfLandscape()
    {
        await new BrowserFetcher().DownloadAsync();

        string path = SeachFile(new DirectoryInfo(Directory.GetCurrentDirectory()), fileFind);

        var launchOptions = new LaunchOptions
        {
            Browser = SupportedBrowser.Chromium,
            Headless = true,
            ExecutablePath = path,
            Args = new[] { "--no-sandbox" }
        };


        using (var browser = await Puppeteer.LaunchAsync(launchOptions))
        using (var page = await browser.NewPageAsync())
        {
            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions
            {
                DisplayHeaderFooter = true,
                Landscape = true,
                PrintBackground = true,
                Format = PaperFormat.A4,
                FooterTemplate = footerTemplate,
                HeaderTemplate = headerTemplate,
                MarginOptions = marginOption,
                OmitBackground = true
            };
            return await page.PdfDataAsync(pdfOptions);
        }
    }


    public async Task<byte[]> GeneratePdfPortable()
    {
        await new BrowserFetcher().DownloadAsync();

        string path = SeachFile(new DirectoryInfo(Directory.GetCurrentDirectory()), fileFind);

        var launchOptions = new LaunchOptions
        {
            Browser = SupportedBrowser.Chromium,
            Headless = true,
            ExecutablePath = path,
            Args = new[] { "--no-sandbox" }
        };


        using (var browser = await Puppeteer.LaunchAsync(launchOptions))
        using (var page = await browser.NewPageAsync())
        {
            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions
            {
                DisplayHeaderFooter = true,
                Landscape = false,
                PrintBackground = true,
                Format = PaperFormat.A4,
                FooterTemplate = footerTemplate,
                HeaderTemplate = headerTemplate,
                MarginOptions = marginOption,
                OmitBackground = true
            };
            return await page.PdfDataAsync(pdfOptions);
        }
    }

    public async Task<byte[]> GeneratePdf10_12()
    {
        await new BrowserFetcher().DownloadAsync();

        string path = SeachFile(new DirectoryInfo(Directory.GetCurrentDirectory()), fileFind);

        var launchOptions = new LaunchOptions
        {
            Browser = SupportedBrowser.Chromium,
            Headless = true,
            ExecutablePath = path,
            Args = new[] { "--no-sandbox" }
        };


        using (var browser = await Puppeteer.LaunchAsync(launchOptions))
        using (var page = await browser.NewPageAsync())
        {
            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions
            {
                DisplayHeaderFooter = true,
                Landscape = false,
                PrintBackground = true,
                FooterTemplate = footerTemplate,
                HeaderTemplate = headerTemplate,
                MarginOptions = marginOption,
                Width = "10cm",
                Height = "12cm",
                OmitBackground = true
            };
            return await page.PdfDataAsync(pdfOptions);
        }
    }

public async Task<byte[]> GeneratePdf12_10()
    {
        await new BrowserFetcher().DownloadAsync();

        string path = SeachFile(new DirectoryInfo(Directory.GetCurrentDirectory()), fileFind);

        var launchOptions = new LaunchOptions
        {
            Browser = SupportedBrowser.Chromium,
            Headless = true,
            ExecutablePath = path,
            Args = new[] { "--no-sandbox" }
        };


        using (var browser = await Puppeteer.LaunchAsync(launchOptions))
        using (var page = await browser.NewPageAsync())
        {
            await page.SetContentAsync(html);

            var pdfOptions = new PdfOptions
            {
                DisplayHeaderFooter = true,
                Landscape = true,
                PrintBackground = true,
                FooterTemplate = footerTemplate,
                HeaderTemplate = headerTemplate,
                MarginOptions = marginOption,
                Width = "10cm",
                Height = "12cm",
                OmitBackground = true
            };
            return await page.PdfDataAsync(pdfOptions);
        }
    }

    public static string SeachFile(DirectoryInfo dir, string fileName)
    {
        try
        {
            // Procura o arquivo no diretório atual
            foreach (var file in dir.GetFiles())
            {
                if (file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    return file.FullName; // Retorna o caminho completo do arquivo encontrado
                }
            }

            // Se não foi encontrado, busca nos subdiretórios
            foreach (var subDir in dir.GetDirectories())
            {
                var result = SeachFile(subDir, fileName);
                if (result != null)
                {
                    return result; // Retorna o caminho completo do arquivo se encontrado
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Ignora pastas sem permissão
        }
        catch (DirectoryNotFoundException)
        {
            // Ignora pastas inexistentes
        }

        return null; // Retorna null se o arquivo não foi encontrado
    }
}
