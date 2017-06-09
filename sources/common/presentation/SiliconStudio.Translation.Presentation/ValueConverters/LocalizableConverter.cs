// Copyright (c) 2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;

namespace SiliconStudio.Translation.Presentation.ValueConverters
{
    /// <summary>
    /// Base class for value converters supporting localization.
    /// </summary>
    /// <typeparam name="TConverter"></typeparam>
    public abstract class LocalizableConverter<TConverter> : MarkupExtension, IValueConverter
        where TConverter : LocalizableConverter<TConverter>, new()
    {
        // Keep a cache per assembly (since localization is grouped per assembly)
        private static readonly Dictionary<Assembly, LocalizableConverter<TConverter>> Cache = new Dictionary<Assembly, LocalizableConverter<TConverter>>();

        /// <summary>
        /// The assembly to lookup the translation.
        /// </summary>
        protected Assembly Assembly { get; private set; }

        /// <inheritdoc />
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <inheritdoc />
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // By default, a localizable converter is one-way only.
            throw new NotSupportedException($"ConvertBack is not supported by this {nameof(IValueConverter)}.");
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var rootProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            var asm = rootProvider.RootObject.GetType().Assembly;
            if (!Cache.TryGetValue(asm, out var converter))
            {
                converter = new TConverter { Assembly = asm };
                Cache.Add(asm, converter);
            }
            return converter;
        }
    }
}
