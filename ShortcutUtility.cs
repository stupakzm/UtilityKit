namespace UtilityKit {
    #region ShortcutUtility
    public class ShortcutUtility {
        public static string GetShortcutTarget(string shortcutPath, bool fromStart = false) {
            int targetStartIndex = 0;
            int targetEndIndex = 0;
            try {
                shortcutPath = shortcutPath.Trim('\"');

                byte[] shortcutData = File.ReadAllBytes(shortcutPath);

                if (IsValidShortcut(shortcutData)) {
                    if (fromStart) {
                        targetStartIndex = FindOffset(shortcutData, System.Text.Encoding.ASCII.GetBytes("C:\\"), System.Text.Encoding.ASCII.GetBytes(".exe"), out int targetEndIndexOut);
                        targetEndIndex = targetEndIndexOut;
                    }
                    else {
                        targetStartIndex = FindOffsetReverse(shortcutData, System.Text.Encoding.ASCII.GetBytes("C:\\"), System.Text.Encoding.ASCII.GetBytes(".exe"), out int targetEndIndexOut);
                        targetEndIndex = targetEndIndexOut;
                    }
                    string targetPath = System.Text.Encoding.ASCII.GetString(shortcutData, targetStartIndex, targetEndIndex);
                    return targetPath;
                }
                else {
                    throw new ArgumentException("Error: Not a valid shortcut file.");
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        private static bool IsValidShortcut(byte[] data) {
            return data.Length > 4 && data[0] == 0x4C && data[1] == 0x00 && data[2] == 0x00 && data[3] == 0x00;
        }

        private static int FindOffset(byte[] data, byte[] patternStart, byte[] patternEnd, out int endOffset) {
            endOffset = 0;

            for (int i = 0; i < data.Length - patternStart.Length; i++) {
                bool matchStart = true;
                int indexFoundAllEndBytes = 0;

                for (int j = 0; j < patternStart.Length; j++) {
                    if (data[i + j] != patternStart[j]) {
                        matchStart = false;
                        break;
                    }
                }

                if (matchStart) {
                    endOffset = 0;

                    for (int k = 0; k < data.Length - i; k++) {
                        if (data[i + k] != patternEnd[indexFoundAllEndBytes]) {
                            indexFoundAllEndBytes = 0;
                            continue;
                        }
                        else {
                            indexFoundAllEndBytes++;

                            if (indexFoundAllEndBytes == patternEnd.Length) {
                                endOffset = k + 1;
                                return i;
                            }
                        }
                    }
                }
            }

            return -1;
        }

        private static int FindOffsetReverse(byte[] data, byte[] patternStart, byte[] patternEnd, out int count) {
            count = 0;

            for (int i = data.Length - 1; i >= 0; i--) {
                bool matchEnd = true;
                int indexFoundAllStartBytes = 0;

                for (int j = 0; j < patternEnd.Length; j++) {
                    if (data[i - j] != patternEnd[patternEnd.Length - 1 - j]) {
                        matchEnd = false;
                        break;
                    }
                }

                if (matchEnd) {
                    for (int k = i; k >= 0; k--) {
                        if (data[k] != patternStart[patternStart.Length - 1 - indexFoundAllStartBytes]) {
                            indexFoundAllStartBytes = 0;
                            continue;
                        }
                        else {
                            indexFoundAllStartBytes++;

                            if (indexFoundAllStartBytes == patternStart.Length - 1) {
                                count = i - k + 2;
                                return k - 1;
                            }
                        }
                    }
                }
            }

            return -1;
        }
    }
    #endregion
}