using UnityEngine;

namespace JFramework.Interface
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 对象变换
        /// </summary>
        Transform transform { get; }
        
        /// <summary>
        /// 游戏对象
        /// </summary>
        GameObject gameObject { get; }

        /// <summary>
        /// 实体更新
        /// </summary>
        void Update();
    }

    /// <summary>
    /// 角色接口
    /// </summary>
    public interface ICharacter : IEntity
    {
    }
}