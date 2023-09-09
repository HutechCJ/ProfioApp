class LocalStorageService {
  public set<T>(key: string, value: T) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public get<T, D>(key: string, defaultValue: D): T {
    const item = localStorage.getItem(key) || JSON.stringify(defaultValue);
    return JSON.parse(item);
  }

  public remove(key: string) {
    localStorage.removeItem(key);
  }

  public clear() {
    localStorage.clear();
  }
}

const localStorageService = new LocalStorageService();

export default localStorageService;
