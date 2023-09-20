﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Rocosa_Utilidades
{
    public static class SessionExtensions
    {
        // stored the session
        public static void Set<T>(this ISession session, string key, T value)
        { 
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // retrieve the session
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
