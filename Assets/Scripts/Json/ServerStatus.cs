﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Assets.Scripts.Json;
//
//    var serverStatus = ServerStatus.FromJson(jsonString);

namespace Assets.Scripts.Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ServerStatus
    {
        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("players")]
        public Players Players { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("extra")]
        public Extra[] Extra { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Extra
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Players
    {
        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("protocol")]
        public long Protocol { get; set; }
    }

    public partial class ServerStatus
    {
        public static ServerStatus FromJson(string json) => JsonConvert.DeserializeObject<ServerStatus>(json, Settings);
        public static string ToJson(ServerStatus self) => JsonConvert.SerializeObject(self, Settings);

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}