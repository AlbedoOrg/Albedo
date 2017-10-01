# How to contribute to Albedo

Albedo is currently being developed in C# on .NET 3.5 (in order to ensure as broad reach as possible) and .NET Standard 2.0 using Visual Studio 2017 with xUnit.net as the unit testing framework. So far, all development has been done with TDD, so there's a high degree of code coverage, and it would be preferable to keep it that way.

## Dependencies

All the external dependencies are restored during the build and don't need to be committed to the repository. If you would like to work with project offline, ensure to trigger a build while you are still online so dependencies are cached. To trigger a build run the `build.cmd` file located in the root directory of the repo.

## Verification

There is currently a single solution to be found under the \Src folder, but be aware that the final verification step before pushing to the repository is to successfully build the `Build.fsx` file. This can be done by running the `Build.cmd` file located in the repository root.

As part of the verification build, Visual Studio Code Analysis is executed in a configuration that treats warnings as errors. No CA warnings should be suppressed unless the documented conditions for suppression are satisfied. Otherwise, a documented agreement between at least two active developers of the project should be reached to allow a suppression of a non-suppressable warning.

## Pull requests

When developing for Albedo, please respect the coding style already present. Look around in the source code to get a feel for it.

Please keep line lengths under 120 characters. Line lengths over 120 characters doen't fit into the standard GitHub code listing window, so it requires vertical scrolling to review.

Also, please follow the [Open Source Contribution Etiquette](http://tirania.org/blog/archive/2010/Dec-31.html). Albedo is a fairly typical open source project: if you want to contribute, start by [creating a fork](http://help.github.com/fork-a-repo/) and [sending a pull request](http://help.github.com/send-pull-requests/) when you have something you wish to commit. When creating pull requests, please keep the Single Responsibility Principle in mind. A pull request that does a single thing very well is more likely to be accepted. See also the article [The Rules of the Open Road](http://blog.half-ogre.com/posts/software/rules-of-the-open-road) for more good tips on working with OSS and Pull Requests.

For complex pull requests, you are encouraged to first start a discussion on the [issue list](https://github.com/ploeh/Albedo/issues). This can save you time, because the Albedo regulars can help verify your idea, or point you in the right direction.

Some existing issues are marked with [the jump-in tag](http://nikcodes.com/2013/05/10/new-contributor-jump-in/). These are good candidates to attempt, if you are just getting started with Albedo.

When you submit a pull request, you can expect a response within a day or two. We (the maintainers of Albedo) have day jobs, so we may not be able to immediately review your pull request, but we do make it a priority. Also keep in mind that we may not be in your time zone.

Most likely, when we review pull requests, we will make suggestions for improvement. This is normal, so don't interpret it as though we don't like your pull request. On the contrary, if we agree on the overall goal of the pull request, we want to work *with* you to make it a success.
