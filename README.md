# File/Directory Selector Control

![Version 1.0.0](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg) ![Licence MIT](https://img.shields.io/badge/Licence-MIT-blue.svg)

![image](https://github.com/FredEkstrand/ImageFiles/raw/master/FileDirSelector/ProjectImage.png)

# Overview
The file directory selector is a composite control consisting of a text box and a button.
When the user clicks the button either an OpenFileDialog or FolderBrowserDialog would appear.
Which dialog that appears can be define during design time through the property grid or programmatically.

#### Features
* Open dialog button on the left or right had side, default to right, from the property grid or programmatically.
* Select from the property grid or programmatically for Open file Dialog or Folder Browser Dialog to show when button is pressed.
* Raises text change event when text box text has changed.
* Select from property grid or programmatically to shorten path in the text box display if too long. This doesn't change the actual path just how its displayed in the text box.

# Getting started
The souce code is written in C# and targeted for the .Net Framework 4.0 and later. Download the entire project and compile.

# Usage
In the first screen shot below, shows two FileDirSelector with the top one have the button on the default right hand side and the bottom on the left hand side.

![Project type](https://github.com/FredEkstrand/ImageFiles/raw/master/fdc1.png )

In the second screen shot below shows the select file path in the top FileDirSelector text box too short to show the entire path and is shorten.
The bottom FileDirSelector shows the text box long enough to show the entire path to the file.The displayed shorten path doesn't alter the original path and can easily be retrieve programmability.

![Project type](https://github.com/FredEkstrand/ImageFiles/raw/master/fdc3.png )

# Code Documentation
MSDN-style code documentation [here](#).

# History
 1.0.0 Initial release into the wild.

# Contributing
If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are always welcome.

# Contact
Fred Ekstrand
email: fredekstrandgithub@gmail.com

# Licensing
This project is licensed under the MIT License - see the LICENSE.md file for details.
