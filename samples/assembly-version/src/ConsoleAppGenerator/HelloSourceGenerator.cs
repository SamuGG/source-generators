﻿using Microsoft.CodeAnalysis;

namespace ConsoleAppGenerator
{
    [Generator]
    public class HelloSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var assemblyVersion = context.Compilation.Assembly.Identity.Version;

            // Build up the source code
            string source = $@"// <auto-generated/>
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public static partial class {mainMethod.ContainingType.Name}
    {{
        static partial void GetAssemblyVersionFromSourceGenerator()
        {{
            Console.WriteLine($""Version from source generator: '{assemblyVersion}'"");
        }}
    }}
}}
";
            var typeName = mainMethod.ContainingType.Name;

            // Add the source code to the compilation
            context.AddSource($"{typeName}.g.cs", source);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}