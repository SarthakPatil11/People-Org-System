using System.Collections;
using Newtonsoft.Json;

namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Class for handling the filters.
    /// </summary>
    public class FilterHandler
    {
        /// <summary>
        /// Method to generate filter.
        /// </summary>
        /// <param name="strField"> To take the field name. </param>
        /// <param name="strOpretor"> To take the opretor. </param>
        /// <param name="dymcValue"> To take the value. </param>
        /// <returns> IList opject of filter. </returns>
        public static IList GenerateFilter(string strField, string strOpretor, dynamic dymcValue)
        {
            string filterSerialized = string.Format(Constants.FILTER_SERIALIZED, strField, strOpretor, dymcValue);
            var filter = JsonConvert.DeserializeObject(filterSerialized) as IList;

            return filter;
        }
    }
}
