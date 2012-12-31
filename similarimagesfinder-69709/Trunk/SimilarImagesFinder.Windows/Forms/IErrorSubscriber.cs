namespace EyeOpen.SimilarImagesFinder.Windows.Forms
{
    using System;

    public interface IErrorSubscriber
    {
        void RegisterError(Exception lastError);
    }
}