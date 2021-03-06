# z80emu

![Workflow](https://github.com/essenbee/z80emu/workflows/.NET%20Core/badge.svg)
![Licence](https://img.shields.io/github/license/essenbee/z80emu)
![Twitch](https://img.shields.io/twitch/status/codebasealpha)
![Twitter](https://img.shields.io/twitter/follow/codebasealpha?label=Follow&style=social)
![Nuget](https://img.shields.io/nuget/v/Essenbee.Z80)

Building a Z80 emulator in C# 7.3 as a .NET Standard 2.0 Class Library using the Test Driven Development (TDD) mindset. The aim is to create an accurate emulation of the Z80 CPU as a nuget package for general use. We will then go on to use the emulated CPU in a larger project to emulate a particular machine, a ZX Spectrum probably, since it has a very simple architecture, and the project is not really about creating such an emulator. Development will happen both on and off my Twitch live-coding stream, https://twitch.tv/codebasealpha.

Known as Project Z, coverage of this project begins from Episode 66. Use the link above to follow me on Twitch and be notifed when I am streaming (normally twice a week on Mondays and Wednesdays, but I sometimes run unscheduled pop-up streams).

**2020-03-15**: Essenbee.Z80 nuget package released!

## Sample Project

The included project *Essenbee.Spectrum48* is an example implementation using the Z80 emulator. It is a very simple and currently quite limited emulation of a ZX Spectrum 48K. It is possible to program in BASIC using this emulation. At the time of writing, I have successfully run the following games on the emulation (as yet, without sound):

- [X] King's Ransom (an "illustrated" text adventure)
- [X] Dizzy II - Treasure Island Dizzy
- [X] Galaxians (shoot-em up)
- [X] Pyramid (shoot-em up)
- [X] Lords of Midnight (strategy)

Who knows how sophisticated (or not) it might become over time? If you would like to contribute code for the sample (or any other project), **why not submit a Pull Request?** Check out the Issues list for things I need help with.

![image](https://user-images.githubusercontent.com/7979108/72829874-7fbba480-3c77-11ea-88ce-17c31865ad5c.png)

![image](https://user-images.githubusercontent.com/7979108/72908129-f9fa3080-3d2c-11ea-8b3e-2fde99422991.png)

### YouTube

The videos for Project Z episodes are archived on YouTube [here](https://www.youtube.com/channel/UCFFtfkaWjMb9UMDpPVnC1Sg). Please subscribe to my YouTube channel to get notified when new videoes are uploaded. Subscribing is free and it helps me out a lot.
