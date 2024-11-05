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
 * @interface UpdateOfferItemCommand
 */
export interface UpdateOfferItemCommand {

    /**
     * @type {OfferItemId}
     * @memberof UpdateOfferItemCommand
     */
    id: OfferItemId;

    /**
     * @type {OfferId}
     * @memberof UpdateOfferItemCommand
     */
    offerId: OfferId;

    /**
     * @type {string}
     * @memberof UpdateOfferItemCommand
     */
    details: string | null;

    /**
     * @type {Quantity}
     * @memberof UpdateOfferItemCommand
     */
    quantity: Quantity;

    /**
     * @type {number}
     * @memberof UpdateOfferItemCommand
     */
    unitPrice: number;

    /**
     * @type {CatalogItemId}
     * @memberof UpdateOfferItemCommand
     */
    catalogItemId?: CatalogItemId;
}
