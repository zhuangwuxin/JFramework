using JFramework.Interface;
using Sirenix.OdinInspector;

namespace JFramework
{
    /// <summary>
    /// 控制器的抽象类
    /// </summary>
    /// <typeparam name="TEntity">实现了IEntity接口的类型</typeparam>
    public abstract class Controller<TEntity> : SerializedScriptableObject, IController where TEntity : IEntity
    {
        /// <summary>
        /// 控制器的所有者
        /// </summary>
        public TEntity owner;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        protected virtual void Spawn() { }

        /// <summary>
        /// 控制器初始化
        /// </summary>
        /// <param name="owner">传入所有者</param>
        void IController.Spawn(IEntity owner)
        {
            this.owner = (TEntity)owner;
            Spawn();
        }
    }
}