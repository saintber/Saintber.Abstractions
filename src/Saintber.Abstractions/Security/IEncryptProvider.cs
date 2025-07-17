using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saintber.Abstractions.Security
{
    /// <summary>
    /// 資料加密提供者介面。
    /// </summary>
    /// <typeparam name="TRaw">原始資料型別。</typeparam>
    /// <typeparam name="TEncrypt">加密資料型別。</typeparam>
    public interface IEncryptProvider<TRaw, TEncrypt>
    {
        /// <summary>
        /// 將原始資料加密為加密資料。
        /// </summary>
        /// <param name="input">輸入原始資料。</param>
        /// <returns>加密資料。</returns>
        Task<TEncrypt> EncryptProvider(TRaw input);
    }

    /// <summary>
    /// 資料加密提供者介面，使用字串作為原始資料和加密資料的型別。
    /// </summary>
    public interface IEncryptProvider : IEncryptProvider<string, string> { }

    /// <summary>
    /// 資料解密提供者介面。
    /// </summary>
    /// <typeparam name="TRaw">原始資料型別。</typeparam>
    /// <typeparam name="TEncrypt">加密資料型別。</typeparam>
    public interface IDecryptProvider<TRaw, TEncrypt>
    {
        /// <summary>
        /// 將加密資料解密為原始資料。
        /// </summary>
        /// <param name="input">輸入加密資料。</param>
        /// <returns>原始資料。</returns>
        Task<TRaw> DecryptProvider(TEncrypt input);
    }

    /// <summary>
    /// 資料解密提供者介面，使用字串作為原始資料和加密資料的型別。
    /// </summary>
    public interface IDecryptProvider : IDecryptProvider<string, string> { }
}
