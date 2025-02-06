# TestDataInator

TestDataInator is a .NET Web API that you can request to generate and manage test data for various applications. It supports both OpenAI and locally installed Ollama models, with configuration details provided in user secrets.

## Features

- Generate test data based on predefined templates
- Customizable data generation rules
- Integration with popular testing frameworks
- Easy-to-use command-line interface

## Configuration

To configure the API to use OpenAI or a locally installed Ollama model, add the necessary information to your user secrets. For example:

```sh
dotnet user-secrets set "OOpenAIKey" "your_openai_api_key"
dotnet user-secrets set "OllamaModel" "path_to_your_ollama_model"
```
