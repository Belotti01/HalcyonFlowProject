using HalcyonFlowProject.Data.Enums;

namespace HalcyonFlowProject.Logic.Extensions {
    public static class EnumExtensions {
        /// <summary>
        ///     Get the TailwindCSS class identified by this <see cref="TextSize"/>.
        /// </summary>
        /// <param name="size">The resulting text size.</param>
        /// <returns>A <see langword="string"/> identifying the TailwindCSS class tied to
        /// the value of the <see cref="TextSize"/>
        /// </returns>.
        public static string ToCssClass(this TextSize size) {
            return "tw-text-" + size switch {
                TextSize.XS => "xs",
                TextSize.S => "sm",
                TextSize.L => "lg",
                TextSize.XL => "xl",
                TextSize.XL2 => "2xl",
                TextSize.XL3 => "3xl",
                TextSize.XL4 => "4xl",
                TextSize.XL5 => "5xl",
                TextSize.XL6 => "6xl",
                TextSize.XL7 => "7xl",
                TextSize.XL8 => "8xl",
                TextSize.XL9 => "9xl",
                _ => "base"
            };
        }
    }
}
