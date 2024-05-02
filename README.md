# UtilityKit

UtilityKit is a dynamic library in C# designed to offer utilities in two main areas: shortcut handling and string similarity measurement. This document provides an overview and examples of how to utilize these utilities within your own projects.

## Features

### ShortcutUtility

Provides methods to resolve the target path of a shortcut file (.lnk). It supports retrieving the target path either from the start of the shortcut or by searching from the end backwards.

- **GetShortcutTarget**: Resolves the target path of a given shortcut file.

### StringSimilarity

Offers several algorithms to measure the similarity between two strings. This includes exact matching, Jaccard similarity, Levenshtein distance, N-gram similarity, and cosine similarity.

- **Tokenize**: Splits a string into a set of tokens.
- **ExactMatch**: Checks if two strings are exactly the same.
- **CalculateJaccardSimilarity**: Measures the similarity between two strings based on the Jaccard index.
- **CalculateLevenshteinDistance**: Computes the Levenshtein distance between two strings.
- **CalculateNGramSimilarity**: Calculates the similarity between two strings using N-gram analysis.
- **CalculateCosineSimilarity**: Determines the cosine similarity between two strings.

## Installation

Add the `UtilityKit.dll` to your C# project references. Ensure the `.dll` is properly located where your project can access it during runtime.

## Usage

### Using ShortcutUtility

```csharp
string shortcutPath = "C:\\path\\to\\your\\shortcut.lnk";
bool fromStart = true; // or false to search from end to shortcut
string targetPath = UtilityKit.ShortcutUtility.GetShortcutTarget(shortcutPath, fromStart);
Console.WriteLine("Target Path: " + targetPath);
