using ChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Interfaces
{
    public interface IOllamaApiClient
    {
        Task<OllamaResponse> GenerateAsync(OllamaRequest request);
        Task<bool> TestConnectionAsync(string modelName);
    }
}
