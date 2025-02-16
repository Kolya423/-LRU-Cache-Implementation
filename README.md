Data Structures Used:

Dictionary (HashMap) → Stores (key, node) for O(1) access.
LinkedList → Keeps track of the usage order (most recently used at the front, least recently used at the back).
Get(key):

If the key exists, move it to the front (most recently used) and return the value.
If not, return -1.
Put(key, value):

If the key already exists → Remove it from the list and dictionary.
If the cache is full, remove the least recently used (LRU) element (last in the list).
Add the new (key, value) to the front of the list and update the dictionary.
This ensures both get() and put() operations work in O(1) time.
