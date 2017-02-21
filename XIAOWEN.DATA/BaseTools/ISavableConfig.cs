namespace XIAOWEN.DATA.BaseTools
{
    public interface ISavableConfig
    {
        /// <summary>
        /// 保存到配置中
        /// </summary>
        /// <returns></returns>
        bool Save(bool isOnlySysInfo);
    }
}
