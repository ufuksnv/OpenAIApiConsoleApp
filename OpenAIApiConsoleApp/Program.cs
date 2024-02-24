using System;
using Azure;
using Azure.AI.OpenAI;

namespace OpenAIConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Mesajınızı yazınız (en fazla 50 kelime):");

            string prompt = Console.ReadLine();

            if (string.IsNullOrEmpty(prompt) || prompt.Length > 50)
            {
                Console.WriteLine("Hata: Mesaj boş olamaz veya en fazla 50 karakter içerebilir.");
            }
            else
            {

                OpenAIClient client = new OpenAIClient("Your Key");
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = "gpt-3.5-turbo-0125",
                    Messages =
                      {
                       new ChatRequestSystemMessage("Sen türk dil kurumunda çalışan bir asistansın ve görevin cümledeki yanlış kelimeleri düzeltmek."),

                       new ChatRequestUserMessage(prompt),

                      }
                };

                Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
                Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");

            }
        }
    }
}
