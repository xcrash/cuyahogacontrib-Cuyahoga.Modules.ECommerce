using System;
namespace Cuyahoga.Modules.ECommerce.DataAccess {

    /// <summary>
    /// Functionality for common simple data access. Extended to use additional key types
    /// </summary>
    public interface IExtCommonDao : Cuyahoga.Core.DataAccess.ICommonDao {

        /// <summary>
        /// Get a single instance from the database by type and primary key. Optionally indicate if the
        /// object may be null when it is not found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        object GetObjectById(Type type, short id, bool allowNull);

        /// <summary>
        /// Get a single instance from the database by type and primary key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        object GetObjectById(Type type, string id);

        /// <summary>
        /// Get a single instance from the database by type and primary key. Optionally indicate if the
        /// object may be null when it is not found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        object GetObjectById(Type type, string id, bool allowNull);

        /// <summary>
        /// Get a single instance from the database by type and primary key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        object GetObjectById(Type type, long id);

        /// <summary>
        /// Get a single instance from the database by type and primary key. Optionally indicate if the
        /// object may be null when it is not found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        object GetObjectById(Type type, long id, bool allowNull);

        /// <summary>
        /// Get a single instance from the database by type and primary key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        object GetObjectById(Type type, short id);
    }
}