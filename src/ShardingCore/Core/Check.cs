using System;
using System.Diagnostics.CodeAnalysis;

namespace ShardingCore.Core
{
/*
* @Author: xjm
* @Description:
* @Date: Saturday, 14 August 2021 14:19:29
* @Email: 326308290@qq.com
*/
    public class Check
    {
        [ExcludeFromCodeCoverage]
        private Check(){}
        /// <summary>
        /// ����Ϊ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T NotNull<T>(T value,string parameterName)
        {
            if(value==null)
                throw new ArgumentNullException(parameterName);

            return value;
        }
        /// <summary>
        /// Ӧ��Ϊ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T ShouldNull<T>(T value, string parameterName)
        {
            if (value != null)
                throw new ArgumentNullException(parameterName);

            return value;
        }
    }
}