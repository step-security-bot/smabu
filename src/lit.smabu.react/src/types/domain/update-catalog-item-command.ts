/* tslint:disable */
/* eslint-disable */
/**
 * LIT.Smabu.API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

import { CatalogId } from './catalog-id';
import { CatalogItemId } from './catalog-item-id';
import { CatalogItemPrice } from './catalog-item-price';
import { Unit } from './unit';
 /**
 * 
 *
 * @export
 * @interface UpdateCatalogItemCommand
 */
export interface UpdateCatalogItemCommand {

    /**
     * @type {CatalogItemId}
     * @memberof UpdateCatalogItemCommand
     */
    catalogItemId?: CatalogItemId;

    /**
     * @type {CatalogId}
     * @memberof UpdateCatalogItemCommand
     */
    catalogId?: CatalogId;

    /**
     * @type {string}
     * @memberof UpdateCatalogItemCommand
     */
    name?: string | null;

    /**
     * @type {string}
     * @memberof UpdateCatalogItemCommand
     */
    description?: string | null;

    /**
     * @type {boolean}
     * @memberof UpdateCatalogItemCommand
     */
    isActive?: boolean;

    /**
     * @type {Unit}
     * @memberof UpdateCatalogItemCommand
     */
    unit?: Unit;

    /**
     * @type {Array<CatalogItemPrice>}
     * @memberof UpdateCatalogItemCommand
     */
    prices?: Array<CatalogItemPrice> | null;
}
