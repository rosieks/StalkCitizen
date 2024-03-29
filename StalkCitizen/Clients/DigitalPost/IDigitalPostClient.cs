// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StalkCitizen.Clients.DigitalPost
{
    using Microsoft.Rest;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    public partial interface IDigitalPostClient : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }


        /// <summary>
        /// Sends a single text message to a citizen/company.
        /// </summary>
        /// <param name='subscriptionId'>
        /// Id of LoGIC subscription
        /// </param>
        /// <param name='request'>
        /// Request body
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<SendMessageResponse>> SendSingleMessageWithHttpMessagesAsync(System.Guid subscriptionId, SendMessageRequest request = default(SendMessageRequest), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sends a single document to a citizen/company.
        /// </summary>
        /// <param name='subscriptionId'>
        /// Id of LoGIC subscription
        /// </param>
        /// <param name='request'>
        /// Request body
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<SendMessageResponse>> SendSingleDocumentWithHttpMessagesAsync(System.Guid subscriptionId, SendDocumentRequest request = default(SendDocumentRequest), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Uploads a single file that could be add as an attachment to the
        /// message later.
        /// </summary>
        /// <param name='subscriptionId'>
        /// Id of LoGIC subscription
        /// </param>
        /// <param name='attachment'>
        /// File added to message
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<UploadAttachmentResponse>> UploadAttachmentWithHttpMessagesAsync(System.Guid subscriptionId, Stream attachment, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
