namespace JSim.AvGL
{
    public class GLVersion
    {
        public GLVersion(
            int major, 
            int minor)
        {
            Major = major;
            Minor = minor;
        }

        public int Major { get; }
        public int Minor { get; }

        public static bool operator ==(GLVersion v1, GLVersion v2)
        {
            if (v1.Major == v2.Major &&
                v1.Minor == v2.Minor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(GLVersion v1, GLVersion v2)
        {
            if (v1.Major == v2.Major &&
                v1.Minor == v2.Minor)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator >(GLVersion v1, GLVersion v2)
        {
            if (v1.Major > v2.Major)
            {
                return true;
            }
            else if (v1.Major == v2.Major &&
                    v1.Minor > v2.Minor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator >=(GLVersion v1, GLVersion v2)
        {
            if (v1.Major > v2.Major)
            {
                return true;
            }
            else if (v1.Major == v2.Major &&
                    v1.Minor >= v2.Minor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(GLVersion v1, GLVersion v2)
        {
            if (v1.Major < v2.Major)
            {
                return true;
            }
            else if (v1.Major == v2.Major &&
                    v1.Minor < v2.Minor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <=(GLVersion v1, GLVersion v2)
        {
            if (v1.Major < v2.Major)
            {
                return true;
            }
            else if (v1.Major == v2.Major &&
                    v1.Minor <= v2.Minor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"V{Major}.{Minor}";
        }
    }
}
