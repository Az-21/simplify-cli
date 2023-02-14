# Simplify CLI

<p align="center">
  <img width="250" height="250" src="https://i.imgur.com/yNhvFMr.png">
</p>
<b>
<p align="center" style = "emphasis">
  A simple tool to mass-simplify (mass-rename) your library
</p>
</b>

<a href="https://github.com/Az-21/filename-simplifier/blob/main/LICENSE" alt="GPL 3.0">
<img src="https://img.shields.io/github/license/Az-21/filename-simplifier?style=for-the-badge" /></a>
<a href="" alt="C#11">
<img src="https://img.shields.io/badge/Built%20With-C%20Sharp-%23630094?style=for-the-badge&logo=c-sharp" /></a>
<a href="" alt=".NET7">
<img src="https://img.shields.io/badge/Built%20On-.NET7-%234E2ACD?style=for-the-badge&logo=dotnet" /></a>

## > Simplify

**Before**
```
(火) [GroupName] Generic.Isekai_ジェネリック異世界_(_2020_)_-_S01E07.[1080p].[HEVC x265 10bit][Multi-Subs].(Weekly).mkv
```
**After**
```
Generic Isekai - S01E07.mkv
```

## Sample I/O

![CLI](https://ucarecdn.com/6e75db27-f596-40db-8060-9f0a2b04600b/cli.png)

## Installation

### Download

- Download the latest release from [GitHub releases](https://github.com/Az-21/simplify-cli/releases/latest).
- Extract the 7zip (`.7z`) archive. I recommend creating a new folder called `Terminal` in `C:\`.
- Do not place these files under `C:\Program Files` or a folder which requires admin privileges.

### Add as System Variable

- Search 'Edit the system environment variables' from the start menu.
- Click on 'Environment Variables'.
- Under the 'User variables' (top panel), select 'Path' and click 'Edit'.
- Click on 'New' and provide the address of the folder containing `Simplify.exe`.
- If you extracted archive in `C:\Terminal`, then the folder containing `Simplify.exe` is `C:\Terminal\Simplify`.

## Run

### Simplify Library

```ps
# Perform a preview run (no changes will be made)
simplify "C:/PathOfLibrary"

# Perform a permanent run
simplify "C:/PathOfLibrary" --rename
```

It is not necessary to provide path of library. By default, program will read the current location of terminal.

```ps
# If a path of library is not provided explicitly, the current terminal location will be considered
C:/Users/Username/Videos> simplify
# ^ This address will be used

# If a path of library is provided explicitly, it will take precedence
C:/Users/Username/Videos> simplify "C:/PathOfLibrary"
# ^ Ignored                         ^ Used as path of library
```

### Include Folders

Unlike files, folders do not have extensions. This makes them difficult to scope. By default, folders are not renamed. This is to prevent undesired changes. To include folders, pass `--includefolders` flag.

```ps
# Perform a preview run with both files and folders (no changes will be made)
simplify "C:/PathOfLibrary" --includefolders

# Perform a permanent run with both files and folders
simplify "C:/PathOfLibrary" --includefolders --rename
```

## Configuration

### Configuration File

- Configuration file can be found in `Simplify\Config\Config.json`.
- Open this file with Notepad (or your IDE).
- Edit the parameters to modify the behavior of app.

### Include Sub-folders

```json
// Config.json
"GetAllDirectories": true, // true or false
```

- `true` ⟶ Include **all** sub-folders.
- `false` ⟶ Rename only the top level contents.

### Extensions

This is a comma-separated list of extension which will be renamed.

```json
// Configuration file
"Extensions": "mkv, mp4, srt, ass, avi, mov, mpeg",
```

```md
Music Video.mp4         // `mp4` is present in `"Extensions"` `->` This file will be simplified
Music Video.mp3         // `mp3` is not present in `"Extensions"` `->` This file will be skipped
```

### Blacklist

This is a **comma-separated** list of words and characters which will be removed from filename (and folder name). This list is **case insensitive**.

```json
// Configuration file
"Blacklist": "., -, _, webrip, x256, HEVC, camrip, nogrp, ddp5, x264",
```

```md
# Input
Movie ABC x256 heVc.mp4

# Output
Movie ABC.mp4
```

Although blacklist contains `HEVC`, `heVc` was also removed because the blacklist is case insensitive.

#### Warning

Avoid adding standard English letters (`"a, b, c"`) and short words in the blacklist. The program is not context aware, and it will remove every occurrence of blacklisted word/character.

### Remove Curly Brackets

```json
// Configuration file
"RemoveCurlyBrackets": true
```

- `true` ⟶ Remove curly brackets and text inside it `{ ... }`.
- `false` ⟶ Keep curly brackets and text inside it.

```md
# Input
{GroupX} Movie ABC {UploaderY}.mp4

# Output
Movie ABC.mp4
```

### Remove Curved Brackets

```json
// Configuration file
"RemoveCurvedBrackets": true
```

- `true` ⟶ Remove curved brackets and text inside it `( ... )`.
- `false` ⟶ Keep curved brackets and text inside it.

```md
# Input
(GroupX) Movie ABC (Uploader Y).mp4

# Output
Movie ABC.mp4
```

### Remove Square Brackets

```json
// Configuration file
"RemoveSquareBrackets": true
```

- `true` ⟶ Remove square brackets and text inside it `[ ... ]`.
- `false` ⟶ Keep square brackets and text inside it.

```md
# Input
[GroupX] Movie ABC [HEVC x256].mp4

# Output
Movie ABC.mp4
```

### Sentence Case

```json
// Configuration file
"SentenceCase": true
```

- `true` ⟶ Convert words to Sentence Case.
- `false` ⟶ Keep case as is.

```md
# Input
a nEw WorLd.mp4

# Output
A New World.mp4
```

### Smart Capitalization

This is an extension option of `"SentenceCase"` to preserve words like `reZero` and `USA`.

```json
// Configuration file
"SmartCapitalization": true
```

`"SentenceCase"` must be `true` for this option to work.

- `true` ⟶ Preserve words like `reZero` and `USA` while converting to Sentence Case.
- `false` ⟶ Keep case as is.

```md
# Input
reZero in a new world.mp4

# Output
reZero In A New World.mp4
```

### Optimize Articles

Convert articles (a, an, the) to lowercase.

```json
// Configuration file
"OptimizeArticles": true
```

- `true` ⟶ Optimize articles.
- `false` ⟶ Keep case of articles as is.

```md
# Input
reZero In A New World.mp4

# Output
reZero in a New World.mp4
```

### Remove Non-ASCII (Non-English) Characters

Remove non-standard characters like Japanese, Cyrillic, and accented Latin characters.

```json
// Configuration file
"RemoveNonAsciiCharacters": true
```

- `true` ⟶ Remove non-ASCII characters.
- `false` ⟶ Keep non-ASCII characters as is.

```md
# Input
Movie 平仮名øøø片仮名øøø漢字.mp4

# Output
Movie.mp4
```

### Append Year

Extract year of release from filename and append it to end with parenthesis. Ignores year `1920` because, most probably, it will be the resolution `(1920x1080)`. This option will work even if the release year is embedded inside brackets `{ ... }`, `( ... )`, `[ ... ]` and they are configured to be removed.

```json
// Configuration file
"AppendYear": true
```

- `true` ⟶ Append year with parenthesis.
- `false` ⟶ Keep year as is.

```md
# Input
Movie 1999 HD.mp4

# Output
Movie HD (1999).mp4
```

### Append Season and/or Episode

Extract season, episode, or season+episode and append it to end. This option will work even if the season and/or episode is embedded inside brackets `{ ... }`, `( ... )`, `[ ... ]` and they are configured to be removed.

```json
// Configuration file
"AppendSeasonAndOrEpisode": true,
"SeasonAndOrEpisodePrefix": "-", // String or character
```

- `true` ⟶ Append season and/or episode with prefix.
- `false` ⟶ Keep season and/or episode as is.

```md
# Input
TV Series S01 HD.mp4
TV Series E999 HD.mp4
TV Series S02E01 HD.mp4

# Output (with "SeasonAndOrEpisodePrefix": "-")
TV Series HD - S01.mp4
TV Series HD - E999.mp4
TV Series HD - S02E01.mp4

# Output (with "SeasonAndOrEpisodePrefix": " ")
TV Series HD S01.mp4
TV Series HD E999.mp4
TV Series HD S02E01.mp4
```

### Note on Appending Functions

If both `"AppendSeasonAndOrEpisode"` `"AppendYear"` are enabled, the year will come first.

```md
# Input
TV Series S01 1997.mp4
TV Series E999 2008.mp4
TV Series S02E01 2023.mp4

# Output (with "SeasonAndOrEpisodePrefix": "-")
TV Series (1997) - S01.mp4
TV Series (2008) - E999.mp4
TV Series (2023) - S02E01.mp4

# Output (with "SeasonAndOrEpisodePrefix": " ")
TV Series (1997) S01.mp4
TV Series (2008) E999.mp4
TV Series (2023) S02E01.mp4
```

### Remove Numbers

```json
// Configuration file
"RemoveNumbers": false
```

- `true` ⟶ Remove any and all numbers.
- `false` ⟶ Keep numbers as is.

```md
# Input
Movie 12345 S01E02.mp4

# Output
Movie S E.mp4
```

This setting is not intended to be used along with append year and append season+episode.

### Convert to Lowercase

```json
// Configuration file
"ConvertToLowercase": false
```

- `true` ⟶ Convert any and all letters to lowercase.
- `false` ⟶ Keep case as is.

```md
# Input
A NEw WorLD.mp4

# Output
a new world.mp4
```
