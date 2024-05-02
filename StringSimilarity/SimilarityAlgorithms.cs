namespace UtilityKit.StringSimilarity {
    public class SimilarityAlgorithms {
        public static HashSet<string> Tokenize(string sentence) {
            // Tokenize the sentence into words
            return new HashSet<string>(sentence.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries));
        }

        #region Exact Matching
        public static bool ExactMatch(string s1, string s2) {
            return s1 == s2;
        }
        #endregion

        #region Jaccard Similarity
        public static double CalculateJaccardSimilarity(string s1, string s2) {

            var set1 = Tokenize(s1);
            var set2 = Tokenize(s2);

            // Calculate intersection
            int intersectionCount = set1.Intersect(set2).Count();

            // Calculate union
            int unionCount = set1.Count + set2.Count - intersectionCount;

            // Calculate similarity
            double similarity = (double)intersectionCount / unionCount;

            return similarity;
        }
        #endregion

        #region Levenshtein Distance
        public static int CalculateLevenshteinDistance(string s1, string s2) {
            int[,] distanceMatrix = new int[s1.Length + 1, s2.Length + 1];

            // Initialize the distance matrix
            for (int i = 0; i <= s1.Length; i++)
                distanceMatrix[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                distanceMatrix[0, j] = j;

            // Fill the distance matrix
            for (int j = 1; j <= s2.Length; j++) {
                for (int i = 1; i <= s1.Length; i++) {
                    if (s1[i - 1] == s2[j - 1])
                        distanceMatrix[i, j] = distanceMatrix[i - 1, j - 1];
                    else
                        distanceMatrix[i, j] = Math.Min(Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1), distanceMatrix[i - 1, j - 1] + 1);
                }
            }

            // Return the Levenshtein distance
            return distanceMatrix[s1.Length, s2.Length];
        }

        #endregion

        #region NGram Similarity
        public static double CalculateNGramSimilarity(string s1, string s2, int n) {
            var ngrams1 = GetNgrams(s1, n);
            var ngrams2 = GetNgrams(s2, n);

            // Calculate intersection and union counts
            int intersectionCount = ngrams1.Intersect(ngrams2).Count();
            int unionCount = ngrams1.Count + ngrams2.Count - intersectionCount;

            // Calculate similarity
            double similarity = (double)intersectionCount / unionCount;

            return similarity;
        }

        private static HashSet<string> GetNgrams(string s, int n) {
            var ngrams = new HashSet<string>();

            // Iterate over the string s to generate n-grams
            for (int i = 0; i <= s.Length - n; i++) {
                ngrams.Add(s.Substring(i, n));
            }

            return ngrams;
        }
        #endregion

        #region Cosine Similarity
        public static double CalculateCosineSimilarity(string s1, string s2) {
            // Tokenize strings into sets of words
            var set1 = Tokenize(s1);
            var set2 = Tokenize(s2);

            // Calculate dot product
            double dotProduct = 0;
            foreach (var word in set1) {
                if (set2.Contains(word)) {
                    dotProduct += 1; // Count 1 for each common word
                }
            }

            // Calculate magnitudes
            double magnitude1 = Math.Sqrt(set1.Count);
            double magnitude2 = Math.Sqrt(set2.Count);

            // Calculate cosine similarity
            double similarity = dotProduct / (magnitude1 * magnitude2);

            return similarity;
        }
        #endregion
    }
}
