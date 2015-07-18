package com.game.bizModule.cd.serv;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import com.game.bizModule.cd.model.CdTimer;
import com.game.bizModule.cd.model.CdTypeEnum;

/**
 * 冷却队列管理器
 * 
 * @author haijiang.jin
 * @since 2014/5/18
 * 
 */
class CdManager {
	/** 玩家角色 UId */
	public long _humanUId = 0L;
	/** 冷却队列字典 */
	public final Map<CdTypeEnum, CdTimer> _cdMap = new ConcurrentHashMap<>();

	/**
	 * 类参数构造器
	 * 
	 * @param humanUId
	 * 
	 */
	CdManager(long humanUId) {
		this._humanUId = humanUId;
	}

	/**
	 * 添加 Cd 计时器
	 * 
	 * @param value 
	 * 
	 */
	public void addCdTimer(CdTimer value) {
		if (value != null) {
			this._cdMap.put(value._cdType, value);
		}
	}

	/**
	 * 获取 Cd 计时器
	 * 
	 * @param cdType
	 * @return 
	 * 
	 */
	public CdTimer getCdTimer(CdTypeEnum cdType) {
		return this._cdMap.get(cdType);
	}
}
