// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StalkCitizen.Clients.DigitalPost
{
    using Models;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for DigitalPostClient.
    /// </summary>
    public static partial class DigitalPostClientExtensions
    {
            /// <summary>
            /// Sends a single text message to a citizen/company.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='request'>
            /// Request body
            /// </param>
            public static SendMessageResponse SendSingleMessage(this IDigitalPostClient operations, System.Guid subscriptionId, SendMessageRequest request = default(SendMessageRequest))
            {
                return operations.SendSingleMessageAsync(subscriptionId, request).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Sends a single text message to a citizen/company.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='request'>
            /// Request body
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<SendMessageResponse> SendSingleMessageAsync(this IDigitalPostClient operations, System.Guid subscriptionId, SendMessageRequest request = default(SendMessageRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SendSingleMessageWithHttpMessagesAsync(subscriptionId, request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Sends a single document to a citizen/company.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='request'>
            /// Request body
            /// </param>
            public static SendMessageResponse SendSingleDocument(this IDigitalPostClient operations, System.Guid subscriptionId, SendDocumentRequest request = default(SendDocumentRequest))
            {
                return operations.SendSingleDocumentAsync(subscriptionId, request).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Sends a single document to a citizen/company.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='request'>
            /// Request body
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<SendMessageResponse> SendSingleDocumentAsync(this IDigitalPostClient operations, System.Guid subscriptionId, SendDocumentRequest request = default(SendDocumentRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SendSingleDocumentWithHttpMessagesAsync(subscriptionId, request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Uploads a single file that could be add as an attachment to the message
            /// later.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='attachment'>
            /// File added to message
            /// </param>
            public static UploadAttachmentResponse UploadAttachment(this IDigitalPostClient operations, System.Guid subscriptionId, Stream attachment)
            {
                return operations.UploadAttachmentAsync(subscriptionId, attachment).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Uploads a single file that could be add as an attachment to the message
            /// later.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='subscriptionId'>
            /// Id of LoGIC subscription
            /// </param>
            /// <param name='attachment'>
            /// File added to message
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<UploadAttachmentResponse> UploadAttachmentAsync(this IDigitalPostClient operations, System.Guid subscriptionId, Stream attachment, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.UploadAttachmentWithHttpMessagesAsync(subscriptionId, attachment, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
