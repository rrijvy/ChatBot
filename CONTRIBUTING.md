# Contributing to ChatBot

Thank you for your interest in contributing to the ChatBot project! We welcome contributions of all kinds.

## How to Contribute

### Reporting Bugs

1. Check if the issue already exists in [GitHub Issues](https://github.com/rrijvy/ChatBot/issues)
2. Create a new issue with:
   - Clear description of the problem
   - Steps to reproduce
   - Expected vs actual behavior
   - System information (.NET version, OS, Ollama version)

### Suggesting Features

1. Check existing issues and discussions
2. Create a new issue with:
   - Clear description of the proposed feature
   - Use case and benefits
   - Implementation ideas (if any)

### Code Contributions

1. **Fork the repository**
2. **Create a feature branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes**:
   - Follow C# coding conventions
   - Add unit tests for new features
   - Update documentation if needed
4. **Test your changes**:
   ```bash
   dotnet build
   dotnet test
   dotnet run
   ```
5. **Commit your changes**:
   ```bash
   git commit -m "Add: your feature description"
   ```
6. **Push and create a pull request**

## Development Guidelines

### Code Style
- Follow Microsoft C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods small and focused

### Testing
- Add unit tests for new functionality
- Ensure all tests pass before submitting PR
- Test with different Ollama models if possible

### Documentation
- Update README.md if adding new features
- Update CONFIGURATION.md for configuration changes
- Add inline comments for complex logic

### Commit Messages
- Use clear, descriptive commit messages
- Start with a verb (Add, Fix, Update, Remove)
- Keep the first line under 72 characters

Example:
```
Add: Support for streaming responses from Ollama
Fix: Configuration validation error handling
Update: README with new installation instructions
```

## Project Structure

When contributing, please maintain the existing architecture:

```
ChatBot/
??? Configuration/    # Settings and config management
??? Services/        # Business logic
??? Models/          # Data models
??? UI/             # User interface
??? Utilities/      # Helper classes
??? Exceptions/     # Custom exceptions
??? Factories/      # Service creation
```

## Getting Help

- Review existing code and documentation
- Ask questions in GitHub Issues
- Check the README.md and CONFIGURATION.md files

## Code of Conduct

- Be respectful and inclusive
- Focus on constructive feedback
- Help others learn and grow
- Follow GitHub's community guidelines

Thank you for contributing to ChatBot! ??