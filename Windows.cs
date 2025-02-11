﻿using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace OrnaLibs
{
    public static partial class Utils
    {
        /// <summary>
        /// Выполнение скрипта в PowerShell
        /// </summary>
        [SupportedOSPlatform("windows")]
        public static void PowerShell(string script)
            {
            var info = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-WindowStyle hidden -Command \"{script.Replace('"', '\'')}\"",
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            };
            Process.Start(info);
        }
        /// <summary>
        /// Выполнение скрипта в PowerShell
        /// </summary>
        [SupportedOSPlatform("windows")]
        public static void CMD(string script) =>
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/q /c \"{script}\"",
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            });

        [SupportedOSPlatform("windows")]
        private static (string, string)[] GetSerialPortsWindows()
        {
            using var registry = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM")!;
            if (registry is null) return [];
            var names = registry.GetValueNames();
            var ports = new (string, string)[names.Length];
            for (var i = 0; i < names.Length; i++)
                ports[i] = ((string)registry.GetValue(names[i])!, names[i].Split('\\')[^1]);
            return ports;
        }
    }
}
