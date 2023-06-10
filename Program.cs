using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main()
    {
        
        string[] siteUrls = {
            "https://www.fcmsantacasasp.edu.br/arearestrita/",
            "https://fcmsantacasasp.edu.br/ead/",
            "https://ead.fcmsantacasasp.edu.br/ava/login/index.php",
            "http://ead.fcmsantacasasp.edu.br/",
            "https://fcmsantacasasp.edu.br/",
            "https://novoportal.fcmsantacasasp.edu.br/FrameHTML/web/app/RH/PortalMeuRH/#/login",
            "http://santamemoria.org.br/",
            "https://interact.fcmsantacasasp.edu.br/apps/cmn/LauncherLogin.jsp",
            "http://www.fcmsantacasasp.edu.br/wp-login.php",
            "http://repositorio.fcmsantacasasp.edu.br/",
            "http://bibliomedtrab.fcmsantacasasp.edu.br/",
            "http://arquivosmedicos.fcmsantacasasp.edu.br/index.php/AMSCSP",
            "http://chamados.fcmsantacasasp.edu.br/glpi/",
            "https://novoportal.fcmsantacasasp.edu.br/frameHTML/web/app/edu/PortalEducacional/login/",
            "https://dcma.fcmsantacasasp.edu.br/admin"
        };

       
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); 
        options.AddArgument("--no-sandbox"); 

        
        string tempFolderPath = Path.Combine(Path.GetTempPath(), "checklist");
        Directory.CreateDirectory(tempFolderPath);

       
        string chromeDriverPath = "C:\\temp\\PrintFaculdade";

        
        using (IWebDriver driver = new ChromeDriver(chromeDriverPath, options))
        {
            // Verificação de disponibilidade dos sites
            foreach (string url in siteUrls)
            {
                try
                {
                    driver.Navigate().GoToUrl(url);
                  
                    System.Threading.Thread.Sleep(2000);
                    
                    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    
                    string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}.png";
                    
                    string filePath = Path.Combine(tempFolderPath, fileName);
                   
                    screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                    Console.WriteLine($"Print da página {url} salvo em {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Não foi possível tirar o print da página {url}. Erro: {ex.Message}");
                }
            }
        }
    }
}
