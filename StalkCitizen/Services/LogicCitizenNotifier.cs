using System;
using System.Threading.Tasks;
using StalkCitizen.Clients.DigitalPost;
using StalkCitizen.Clients.DigitalPost.Models;

namespace StalkCitizen.Services
{
    public class LogicCitizenNotifier : ICitizenNotifier
    {
        private readonly IDigitalPostClient _digitalPostClient;
        private readonly Guid _configurationId;
        private readonly Guid _subscriptionId;

        public LogicCitizenNotifier(IDigitalPostClient digitalPostClient, Guid subscriptionId, Guid configurationId)
        {
            _digitalPostClient = digitalPostClient;
            _configurationId = configurationId;
            _subscriptionId = subscriptionId;
        }

        public async Task NotifyCitizen(string cpr, string message)
        {
            IronPdf.HtmlToPdf renderer = new IronPdf.HtmlToPdf();
            using (var stream = renderer.RenderHtmlAsPdf($"<h1>Hello {cpr}<h1>").Stream)
            {
                var attachment = await _digitalPostClient.UploadAttachmentWithHttpMessagesAsync(_subscriptionId, stream);
                await _digitalPostClient.SendSingleDocumentWithHttpMessagesAsync(
                    _subscriptionId,
                    new SendDocumentRequest
                    {
                        Identifier = cpr,
                        IdentifierType = "cpr",
                        Title = "You have been stalked",
                        ConfigurationId = _configurationId,
                        ContentReferenceId = attachment.Body.ReferenceId,
                        MaterialId = "signering",
                        ContentExtension = "pdf"
                    });
            }
            
            
        }
    }
}