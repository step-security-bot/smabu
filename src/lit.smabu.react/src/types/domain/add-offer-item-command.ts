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

import { CatalogItemId } from './catalog-item-id';
import { OfferId } from './offer-id';
import { OfferItemId } from './offer-item-id';
import { Quantity } from './quantity';
 /**
 * 
 *
 * @export
 * @interface AddOfferItemCommand
 */
export interface AddOfferItemCommand {

    /**
     * @type {OfferItemId}
     * @memberof AddOfferItemCommand
     */
    id: OfferItemId;

    /**
     * @type {OfferId}
     * @memberof AddOfferItemCommand
     */
    offerId: OfferId;

    /**
     * @type {string}
     * @memberof AddOfferItemCommand
     */
    details: string | null;

    /**
     * @type {Quantity}
     * @memberof AddOfferItemCommand
     */
    quantity: Quantity;

    /**
     * @type {number}
     * @memberof AddOfferItemCommand
     */
    unitPrice: number;

    /**
     * @type {CatalogItemId}
     * @memberof AddOfferItemCommand
     */
    catalogItemId?: CatalogItemId | null;
    
}
