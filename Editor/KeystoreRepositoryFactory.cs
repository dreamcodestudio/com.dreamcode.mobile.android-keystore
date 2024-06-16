using System;
using DreamCode.AutoKeystore.Editor.Configuration;

namespace DreamCode.AutoKeystore.Editor
{
    public sealed class KeystoreRepositoryFactory
    {
        public IKeystoreRepository Create(KeystoreRepository repository)
        {
            return repository switch
            {
                KeystoreRepository.EditorPrefs => new EditorPrefsRepository(),
                KeystoreRepository.OsEnvironment => new OsEnvironmentRepository(),
                _ => throw new NotImplementedException($"{nameof(KeystoreRepositoryFactory)}-{repository}")
            };
        }
    }
}