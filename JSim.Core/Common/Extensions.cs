namespace JSim.Core.Common
{
    public static class Extensions
    {
        public static IEnumerable<T> Flatten<T>(
            this IEnumerable<T> e,
            Func<T, IEnumerable<T>> f)
        {
            return e.SelectMany(c => f(c).Flatten(f)).Concat(e);
        }
    }
}
