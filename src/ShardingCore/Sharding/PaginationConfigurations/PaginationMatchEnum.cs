using System;

namespace ShardingCore.Sharding.PaginationConfigurations
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: Wednesday, 01 September 2021 21:27:25
    * @Email: 326308290@qq.com
    */
    [Flags]
    public enum PaginationMatchEnum
    {
        /// <summary>
        /// �����ǵ�ǰ���������
        /// </summary>
        Owner = 1,
        /// <summary>
        /// ֻҪ����һ���Ϳ�����
        /// </summary>
        Named = 1 << 1,
        /// <summary>
        /// ����һ��ƥ��Ϳ�����
        /// </summary>
        PrimaryMatch = 1 << 2
    }
}