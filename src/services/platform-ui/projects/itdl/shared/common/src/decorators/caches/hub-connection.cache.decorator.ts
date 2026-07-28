import { RxHubConnection } from '../../utils/rx-hub-connection/rx-hub-connection';
import { Cache, CacheDecoratorFactory } from './cache.decorator';

export const RxHubConnectionCache: Cache<RxHubConnection> = new Map<string, RxHubConnection>();

export const RxHubConnectionCacheable = CacheDecoratorFactory(RxHubConnectionCache);
