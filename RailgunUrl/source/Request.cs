namespace RailgunUrl;
using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public partial class Request {
    private AppUI appUI;

    public Request(AppUI appUI) {
        this.appUI = appUI;
    }
    
    private async Task<string> SendGETRequest(string url) {
        using (HttpClient client = new HttpClient()) {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }

    private async Task<string> SendPOSTRequest(string url) {
        using(HttpClient client = new HttpClient()) {
            var JSON = appUI.dataInput.Text;
            var data = new StringContent(JSON, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }


    private async Task<string> SendDELETERequest(string url) {
        using(HttpClient client = new HttpClient()) {
        var response = await client.DeleteAsync(url);
          
        var responseContent = await response.Content.ReadAsStringAsync();

        return responseContent;
    }
}
    public async void SubmitButtonClicked(object? sender, EventArgs e) {
        string? response;
        string? http_request = appUI.selectMethodBox.SelectedItem?.ToString();

        string? url = appUI.urlInput.Text;
        if(string.IsNullOrWhiteSpace(url)) {
            MessageBox.Show("Error: URL is invalid");
            return;
        }

         if(!url.StartsWith("https://") && !url.StartsWith("http://")) {
            MessageBox.Show("Error: URL must start with https:// or http://");
            return;
        }
        
        if(url.StartsWith("https://") || url.StartsWith("http://")) {
            switch(http_request) {
                case "GET":
                appUI.resultLog.Text = "";
                appUI.waitingNotify.Text = $"Waiting response in {url} ...";
                try {
                    response = await SendGETRequest(url);
                    appUI.resultLog.Text = $"{response}";
                }
                catch {
                    appUI.resultLog.Text = $"ERR! when trying send GET request to : {url}";
                }
                appUI.waitingNotify.Text = "Result:";
                break;

                case "POST":
                appUI.resultLog.Text = "";
                appUI.waitingNotify.Text = $"Waiting response in {url} ...";
                try {
                    response= await SendPOSTRequest(url);
                    appUI.resultLog.Text = $"{response}";
                } catch {
                    appUI.resultLog.Text = $"ERR! when trying send GET request to: {url}";
                }
                appUI.waitingNotify.Text = "Result:";
                break;

                case "DELETE":
                appUI.resultLog.Text = "";
                appUI.waitingNotify.Text = $"Waiting response in {url} ...";
                try {
                    response = await SendDELETERequest(url);
                    appUI.resultLog.Text = $"{response}";
                } catch {
                    appUI.resultLog.Text = $"Err! when trying send DELETE request to: {url}";
                }
                appUI.waitingNotify.Text = "Result";
                break;
            }
        }
       
    }
}