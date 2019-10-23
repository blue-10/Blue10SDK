using System;
using Xunit;
using Blue10SDK.Json;

namespace Blue10SDKTests.Json
{
    public class Blue10JsonNamingPolicyTests
    {
        #region Constants

        private const string ORIGINAL_NAME = "ThisIsARandomPropertyName";
        private const string EXPECTED = "this_is_a_random_property_name";

        #endregion

        #region Fields

        private readonly Blue10JsonNamingPolicy mNamingPolicy = new Blue10JsonNamingPolicy();

        #endregion

        [Fact]
        public void ConvertName_WhenNameValid_ReturnsConvertedName()
        {
            var actual = mNamingPolicy.ConvertName(ORIGINAL_NAME);

            Assert.Equal(EXPECTED, actual);
        }

        [Fact]
        public void ConvertName_WhenNameEmpty_ReturnsEmpty()
        {
            var actual = mNamingPolicy.ConvertName(string.Empty);

            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void ConvertName_WhenNameNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => mNamingPolicy.ConvertName(null));
        }
    }
}
