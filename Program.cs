using OpenAI;
using OpenAI.Chat;

String token = "";

OpenAIClient client = new OpenAIClient(new Azure.AzureKeyCredential(token));

// https://platform.openai.com/docs/pricing
string modelName = "gpt-4.1-nano-2025-04-14";

ChatClient chatClient = client.GetChatClient(modelName);

/*
A common, base representation of a message provided as input into a chat completion request.

Type - Role - Description
SystemChatMessage - system - Instructions to the model that guide the behavior of future assistant messages.
UserChatMessage - user - Input messages from the caller, typically paired with assistant messages in a conversation.
AssistantChatMessage - assistant - Output messages from the model with responses to the user or calls to tools or functions that are needed to continue the logical conversation.
ToolChatMessage - tool - Resolution information for a ChatToolCall in an earlier AssistantChatMessage that was made against a supplied ChatTool.
FunctionChatMessage - function - Resolution information for a ChatFunctionCall in an earlier AssistantChatMessage that was made against a supplied ChatFunction. Note that functions are deprecated in favor of tool_calls.
*/
string systemMessage = "You are a helpful assistant.";
string userMessage = "What is the capital of France?";

IEnumerable<ChatMessage> messages =
[
    new SystemChatMessage(systemMessage),
    new UserChatMessage(userMessage),
];

Console.WriteLine("System message: " + systemMessage);
Console.WriteLine("User message: " + userMessage);

System.ClientModel.ClientResult<ChatCompletion> chatCompletion = await chatClient.CompleteChatAsync(messages);
ChatCompletion chatCompletionValue = chatCompletion.Value;
ChatMessageContent response = chatCompletionValue.Content;
ChatMessageContentPart responseContent = response.First();
string responseContentText = responseContent.Text;

Console.WriteLine("Chat response: " + responseContentText);