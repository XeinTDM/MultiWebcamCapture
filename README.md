# MultiWebcamCapture
MultiWebcamCapture is a simple C# console application designed to capture screenshots from multiple webcams. It utilizes the Emgu CV and DirectShowLib libraries to interface with the cameras and capture images.

## Features
- Detects available webcams on your system.
- Captures a screenshot from up to 4 webcams simultaneously.
- Saves the captured screenshots as PNG images on the desktop.

## Requirements
- .NET 8.0
- DirectShowLib
- Emgu.CV
- Emgu.CV.Bitmap
- Emgu.CV.runtime.windows
- System.Drawing.Common

## Installation
Clone the repository:
```bash
git clone https://github.com/XeinTDM/MultiWebcamCapture
```
2. Navigate to the project directory:
```bash
cd MultiWebcamCapture
```
3. Restore the required packages:
```bash
dotnet restore
```

## Usage
Run the application:
```bash
dotnet run
```

The application will detect available webcams and capture screenshots from up to 4 webcams, saving the images to your desktop.

## License
This project is released under the [UNLICENSE](Unlicense).
