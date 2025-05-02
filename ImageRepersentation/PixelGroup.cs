
using System;
using System.Collections.Generic;
using System.Collections;
using ColorRepresentation;
using OpenCvSharp;

namespace ImageRepersentation
{
    public class PixelGroup : IEnumerable<SingularPixel> {
        public Mat internalImage { private set;  get; }
        public int Width { 
            get { return internalImage.Width; }
        }
        public int Height {
            get { return internalImage.Height; }
        }
        public int size { 
            get { return this.internalImage.Width * this.internalImage.Height; } 
        }
        public PixelGroup(Mat image) {
            this.internalImage = image;
        }
        public void applyToAllPixels(Action<SingularPixel> action) {
            int width = internalImage.Width;
            int height = internalImage.Height;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) { 
                    Vec3b bgrColor = this.internalImage.At<Vec3b>(y, x);
                    Vec3b rgbColor = new Vec3b();
                    rgbColor[0] = bgrColor[2];
                    rgbColor[1] = bgrColor[1];
                    rgbColor[2] = bgrColor[0];

                    var pixel = new SingularPixel(rgbColor, x, y);
                    action(pixel);
                }
            }
        }
        
        public IEnumerator<SingularPixel> GetEnumerator() {
            int width = internalImage.Width;
            int height = internalImage.Height;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x <= width; x++) { 
                    Vec3b bgr = internalImage.At<Vec3b>(x,y);
                    Vec3b rgb = new Vec3b();
                    rgb[0] = bgr[2];
                    rgb[1] = bgr[1];
                    rgb[2] = bgr[0];
                    var pixel = new SingularPixel(rgb, x, y);
                    yield return pixel;
                }
            }
        }
        

        IEnumerator IEnumerable.GetEnumerator() { 
            return GetEnumerator();
        }
    }
}
