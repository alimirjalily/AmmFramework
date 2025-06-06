﻿using System.Globalization;
using AmmFramework.Extensions.Translations.Abstractions;
using AmmFramework.Extensions.Translations.Parrot.Databases;
using AmmFramework.Extensions.Translations.Parrot.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AmmFramework.Extensions.Translations.Parrot.Services;

public class ParrotTranslator : ITranslator, IDisposable
{
    private readonly string _currentCulture;
    private readonly ParrotSqlRepository _localizer;
    private readonly ILogger<ParrotTranslator> _logger;
    private ITranslator _translatorImplementation;

    public ParrotTranslator(IOptions<ParrotTranslatorOptions> configuration, ILogger<ParrotTranslator> logger)
    {
        _currentCulture = CultureInfo.CurrentCulture.ToString();
        _logger = logger;
        if (string.IsNullOrWhiteSpace(_currentCulture))
        {
            _currentCulture = "en-US";
            _logger.LogInformation("Parrot Translator current culture is null and set to en-US");
        }
        _localizer = new ParrotSqlRepository(configuration.Value, logger);
        _logger.LogInformation("Parrot Translator Start working with culture {Culture}", _currentCulture);
    }


    public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
    public string this[CultureInfo culture, string name] { get => GetString(culture, name); set => throw new NotImplementedException(); }


    public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
    public string this[CultureInfo culture, string name, params string[] arguments] { get => GetString(culture, name, arguments); set => throw new NotImplementedException(); }


    public string this[char separator, params string[] names] { get => GetConcatString(separator, names); set => throw new NotImplementedException(); }
    public string this[CultureInfo culture, char separator, params string[] names] { get => GetConcatString(culture, separator, names); set => throw new NotImplementedException(); }


    public string GetString(string name)
    {
        _logger.LogTrace("Parrot Translator GetString with name {name}", name);
        return _localizer.Get(name, _currentCulture);

    }
    public string GetString(CultureInfo culture, string name)
    {
        _logger.LogTrace("Parrot Translator GetString  with culture {culture} name {name}", culture, name);
        if (culture is null)
            return _localizer.Get(name, _currentCulture);
        else return _localizer.Get(name, culture.ToString());
    }


    public string GetString(string pattern, params string[] arguments)
    {
        _logger.LogTrace("Parrot Translator GetString with pattern {pattern} and arguments {arguments}", pattern, arguments);

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] = GetString(arguments[i]);
        }

        pattern = GetString(pattern);

        for (int i = 0; i < arguments.Length; i++)
        {
            string placeHolder = $"{{{i}}}";
            pattern = pattern.Replace(placeHolder, arguments[i]);
        }

        return pattern;
    }

    public string GetString(CultureInfo culture, string pattern, params string[] arguments)
    {
        _logger.LogTrace("Parrot Translator GetString with culture {culture} and  pattern {pattern} and arguments {arguments}", culture, pattern, arguments);

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] = GetString(culture, arguments[i]);
        }

        pattern = GetString(culture, pattern);

        for (int i = 0; i < arguments.Length; i++)
        {
            string placeHolder = $"{{{i}}}";
            pattern = pattern.Replace(placeHolder, arguments[i]);
        }

        return pattern;
    }

    public string GetConcatString(char separator = ' ', params string[] names)
    {
        _logger.LogTrace("Parrot Translator GetConcatString with separator {separator} and names {names}", separator, names);

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = GetString(names[i]);
        }

        return string.Join(separator, names);
    }

    public string GetConcatString(CultureInfo culture, char separator = ' ', params string[] names)
    {
        _logger.LogTrace("Parrot Translator GetConcatString with culture {culture} and separator {separator} and names {names}", culture, separator, names);

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = GetString(culture, names[i]);
        }

        return string.Join(separator, names);
    }

    public void Dispose()
    {
        _logger.LogInformation("Parrot Translator Stop working with culture {Culture}", _currentCulture);
    }
}