using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main()
    {
        
        string[] siteUrls = {
           "SITES"
        };

       
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); 
        options.AddArgument("--no-sandbox"); 

        
        string tempFolderPath = Path.Combine(Path.GetTempPath(), "checklist");
        Directory.CreateDirectory(tempFolderPath);

       
        string chromeDriverPath = "C:\\temp\\Print";

        
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
