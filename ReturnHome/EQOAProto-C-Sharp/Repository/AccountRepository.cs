namespace ReturnHome.Repository {
    public interface AccountRepository {
        /// <summary> checks the data store, and verifies that the credentials provided are valid for the user provided </summary>
        bool CredentialsAreValid(string accountName, string credentials);
    }
}
